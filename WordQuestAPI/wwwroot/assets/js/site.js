const uri = '../../../../api/WordQuestWord';
let words = [];

function getWords() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayWords(data))
    .catch(error => console.error('Unable to get words.', error));
}

let correctAnswer = "";

function loadRandomWord() {
  fetch(uri)
      .then(response => response.json())
      .then(data => {
          enableOnClick();
          const randomIndex = Math.floor(Math.random() * data.length);
          const randomWord = data[randomIndex];
          document.getElementById('randomWord').innerText = randomWord.enWord;
          correctAnswer = randomWord.frWord;
          // Charger les réponses possibles
          const answers = [randomWord.frWord];
          while (answers.length < 4) {
            const randomIndex = Math.floor(Math.random() * data.length);
            const randomAnswer = data[randomIndex];
            answers.push(randomAnswer.frWord);
          }
          // Mélanger les réponses
          answers.sort(() => Math.random() - 0.5);
          // Afficher les réponses
          for (let i = 0; i < 4; i++) {
            document.getElementById(`answer${i + 1}`).classList = ["answer"]
            document.getElementById(`answer${i + 1}`).innerText = answers[i];
          }
          const validateButton = document.getElementById('validateButton');
          validateButton.classList = ['hidden'];
      })
      .catch(error => console.error('Error:', error));
}

function checkAnswer(index) {
  const selectedAnswer = document.getElementById(`answer${index + 1}`);
  //let answers = document.querySelectorAll('.answer');
  if (selectedAnswer.innerText === correctAnswer) {
    selectedAnswer.classList = ['correct-answer'];
  } else {
    selectedAnswer.classList = ['incorrect-answer'];
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
  validateButton.classList= ['visible'];
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

function addWord() {
  const addFrWordTextbox = document.getElementById('add-fr-word');
  const addEnWordTextbox = document.getElementById('add-en-word');

  const word = {
    frWord: addFrWordTextbox.value.trim(),
    enWord: addEnWordTextbox.value.trim()
  };

  fetch(uri, {
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
  fetch(`${uri}/${id}`, {
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

  fetch(`${uri}/${wordId}`, {
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
