let words = [];

async function getWords() {
  return await fetch(uri + '/api/WordQuestWord/')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json(); // Convertit la réponse en JSON et la retourne
        })
        .catch(error => {
            console.error('Unable to get words.', error);
            throw error; // Propage l'erreur pour la gérer où la fonction est appelée
        });
}

let correctAnswer = "";

async function loadRandomWord() {
  try {
    const response = await fetch(uri + '/api/WordQuestWord/');
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    const data = await response.json();

    enableOnClick();

    const randomIndex = Math.floor(Math.random() * data.length);
    const randomWord = data[randomIndex];
    document.getElementById('randomWord').innerText = randomWord.enWord;
    correctAnswer = randomWord.frWord;

    await showFlower(randomIndex);

    // Charger les réponses possibles
    const answers = [randomWord.frWord];
    while (answers.length < 4) {
      const randomIndex = Math.floor(Math.random() * data.length);
      const randomAnswer = data[randomIndex];
      if (!answers.includes(randomAnswer.frWord)) {
        answers.push(randomAnswer.frWord);
      }
    }

    // Mélanger les réponses
    answers.sort(() => Math.random() - 0.5);

    // Afficher les réponses
    for (let i = 0; i < 4; i++) {
      const answerElement = document.getElementById(`answer${i + 1}`);
      answerElement.classList = ["answer"];
      answerElement.innerText = answers[i];
    }

    const validateButton = document.getElementById('validateButton');
    validateButton.classList.add('hidden');
  } catch (error) {
    console.error('Error:', error);
  }
}

function clearFlower() {
  for (let i = 0; i < 5; i++) {
    if (!document.getElementById(`flower${(i+1)}`).classList.contains('hidden')) {
      document.getElementById(`flower${(i+1)}`).classList.add('hidden');
    }
  }
}

async function showFlower(word_id) {
  clearFlower();
  user = await checkWhoIsLogIn();
  if (user.id) {
    learningstage = await learningStage(user.id, word_id);
    for (let i = 0; i < 5; i++) {
      if (i+1 == learningstage) {
        document.getElementById(`flower${(i+1)}`).classList.remove('hidden');
      }
    }
  }
}

async function learningStage(user_id, word_id) {
  try {
    const response = await fetch(uri + `/api/WordQuestUser/${user_id}/learnedwords/${word_id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (!response.ok) {
        throw new Error('Network response was not ok');
    }

    // Vérifier si la réponse est nulle
    const text = await response.text();
    if (!text) {
        await addWordToLearnedWords(user_id, word_id);
        return 0; // Retourner une valeur par défaut ou indiquer l'absence de données
    }

    // Si la réponse n'est pas nulle, convertir en JSON
    const data = JSON.parse(text);
    return data;
  } catch (error) {
      console.error('Error fetching learning stage:', error);
      throw error;
  }
}

async function updateLearningStage(word, update) {
  user = await checkWhoIsLogIn();
  if (user) {
    word_id = await searchWordIdByFr(word);
    if (word_id) {
      newLearningStage = await learningStage(user.id, word_id) + update;
      await fetch(uri + `/api/WordQuestUser/${user.id}/learnedwords/${word_id}`, {
        method: 'PUT',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(newLearningStage)
      })
      .catch(error => console.error('Unable to update word.', error));
    }
  }
}

async function addWordToLearnedWords(user_id, word_id) {
  await fetch(uri + `/api/WordQuestUser/${user_id}/learnedwords/`, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(word_id)
  })
    .then(response => response.json())
    .catch(error => console.error('Unable to add word.', error));
}

function checkAnswer(index) {
  const selectedAnswer = document.getElementById(`answer${index + 1}`);
  //let answers = document.querySelectorAll('.answer');
  if (selectedAnswer.innerText === correctAnswer) {
    selectedAnswer.classList = ['correct-answer'];
    updateLearningStage(correctAnswer, 1);
  } else {
    selectedAnswer.classList = ['incorrect-answer'];
    updateLearningStage(correctAnswer, -1);
  }
  for (let i = 0; i < 4; i++) {
    const thisAnswer = document.getElementById(`answer${i + 1}`);
    if (thisAnswer.innerText == correctAnswer) {
      thisAnswer.classList = ['correct-answer'];
    } else {
      thisAnswer.classList.add('disabled');
    }
  }

  disableOnClick();

  const validateButton = document.getElementById('validateButton');
  validateButton.classList.remove('hidden');
  return false;
}

function disableOnClick() {
  const answers = document.querySelectorAll('.answers a');
  originalOnClicks = []; // Reset the array
  
  answers.forEach((answer) => {
    // Remove the onclick attribute
    answer.onclick = null;
  });
}

function enableOnClick() {
  const answers = document.querySelectorAll('.answers a');
  
  answers.forEach((answer, index) => {
    // Reapply the original onclick function
    answer.onclick = function() {
      checkAnswer(index);
    }
  });
}

async function searchWordIdByFr(wordFr) {
  return await getWords()
        .then(words => {
            // Vérifie que words est bien un tableau
            if (!Array.isArray(words)) {
                console.error('Error: "words" is not an array.');
                return null;
            }

            // Utilisation de find pour trouver le mot correspondant
            let foundWord = words.find(word => word.frWord === wordFr);

            // Si foundWord est défini, retournez son WordId ; sinon, retournez null
            return foundWord ? foundWord.wordId : null;
        })
        .catch(error => {
            console.error('Error fetching words:', error);
            return null; // Gestion de l'erreur ici
        });
}

async function searchWordIdByEn(wordEn) {
  return await getWords()
        .then(words => {
            // Vérifie que words est bien un tableau
            if (!Array.isArray(words)) {
                console.error('Error: "words" is not an array.');
                return null;
            }

            // Utilisation de find pour trouver le mot correspondant
            let foundWord = words.find(word => word.enWord === wordEn);

            // Si foundWord est défini, retournez son WordId ; sinon, retournez null
            return foundWord ? foundWord.WordId : null;
        })
        .catch(error => {
            console.error('Error fetching words:', error);
            return null; // Gestion de l'erreur ici
        });
}

function addWord() {
  const addFrWordTextbox = document.getElementById('add-fr-word');
  const addEnWordTextbox = document.getElementById('add-en-word');

  const word = {
    frWord: addFrWordTextbox.value.trim(),
    enWord: addEnWordTextbox.value.trim()
  };

  fetch(uri  + '/api/WordQuestWord/', {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(word)
  })
    .then(response => response.json())
    .then(() => {
      getWords();
      addFrWordTextbox.value = '';
      addEnWordTextbox.value = '';
    })
    .catch(error => console.error('Unable to add word.', error));
}

function deleteWord(id) {
  fetch(`${uri}/api/WordQuestWord/${id}`, {
    method: 'DELETE'
  })
  .then(() => getWords())
  .catch(error => console.error('Unable to delete word.', error));
}

function displayEditForm(id) {
  const word = words.find(word => word.wordId === id);
  
  document.getElementById('edit-fr-word').value = word.frWord;
  document.getElementById('edit-en-word').value = word.enWord;
  document.getElementById('edit-id').value = word.wordId;
  document.getElementById('editForm').style.display = 'block';
}

function updateWord() {
  const wordId = document.getElementById('edit-id').value;
  const word = {
    wordId: parseInt(wordId, 10),
    frWord: document.getElementById('edit-fr-word').value.trim(),
    enWord: document.getElementById('edit-en-word').value.trim()
  };

  fetch(`${uri}/api/WordQuestWord/${wordId}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(word)
  })
  .then(() => getWords())
  .catch(error => console.error('Unable to update word.', error));

  closeInput();

  return false;
}

function closeInput() {
  document.getElementById('editForm').style.display = 'none';
}

function _displayCount(wordCount) {
  const name = (wordCount === 1) ? 'word' : 'words';

  document.getElementById('counter').innerText = `${wordCount} ${name}`;
}

function _displayWords(data) {
  const tBody = document.getElementById('words');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(word => {
    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${word.wordId})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteWord(${word.wordId})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    let frWordNode = document.createTextNode(word.frWord);
    td1.appendChild(frWordNode);

    let td2 = tr.insertCell(1);
    let enWordNode = document.createTextNode(word.enWord);
    td2.appendChild(enWordNode);

    let td3 = tr.insertCell(2);
    td3.appendChild(editButton);

    let td4 = tr.insertCell(3);
    td4.appendChild(deleteButton);
  });

  words = data;
}
