function getUsers() {
  fetch(uri + '/api/WordQuestUser')
    .then(response => response.json())
    .then(data => _displayUsers(data))
    .catch(error => console.error('Unable to get users.', error));
}

function getUser(id) {
  return fetch(uri + `/api/WordQuestUser/${id}`, {
    method: 'GET',
  })
  .then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.json();
  })
  .then(data => {
    return data;
  })
  .catch(error => {
    console.error('Unable to get user.', error);
    throw error;
  });
}

async function getUserByName(username) {
  return await fetch(uri + `/api/WordQuestUser/get_by_name/${username}`, {
    method: 'GET',
  })
  .then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.json();
  })
  .then(data => {
    return data;
  })
  .catch(error => {
    console.error('Unable to get user.', error);
    throw error;
  });
}


function addUser() {
  const addUserNameTextbox = document.getElementById('add-user-name');
  const addUserEmailTextbox = document.getElementById('add-user-email');
  const addUserPasswordTextbox = document.getElementById('add-user-password');
  const addConfirmPasswordTextbox = document.getElementById('add-confirm-password');

  if (addUserPasswordTextbox.value.trim() === addConfirmPasswordTextbox.value.trim()) {
    const user = {
        userName: addUserNameTextbox.value.trim(),
        userEmail: addUserEmailTextbox.value.trim(),
        userPassword: addUserPasswordTextbox.value.trim()
      };
  
    fetch(uri + '/api/WordQuestUser', {
        method: 'POST',
        headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(response => response.json())
        .then(() => {
        getUsers();
        addUserNameTextbox = '';
        addUserEmailTextbox = '';
        addUserPasswordTextbox = '';
        addConfirmPasswordTextbox = '';
        })
        .catch(error => console.error('Unable to add user.', error));
    
  } else {
    console.error('Passwords do not match.');
  }
}

function deleteUser(id) {
  fetch(uri + `/api/WordQuestUser/${id}`, {
    method: 'DELETE'
  })
  .then(() => getWords())
  .catch(error => console.error('Unable to delete user.', error));
}

function displayEditForm(id) {
  const user = users.find(user => user.userId === id);
  
  document.getElementById('edit-user-name').value = user.userName;
  document.getElementById('edit-user-email').value = user.userEmail;
  document.getElementById('edit-user-password').value = user.userPassword;
  document.getElementById('edit-id').value = user.userId;
  document.getElementById('editForm').style.display = 'block';
}

function updateUser() {
  const userId = document.getElementById('edit-id').value;
  const editUserNameTextbox = document.getElementById('edit-user-name');
  const editdUserEmailTextbox = document.getElementById('edit-user-email');
  const editUserPasswordTextbox = document.getElementById('edit-user-password');
  const editConfirmPasswordTextbox = document.getElementById('edit-confirm-password');

    if (editUserPasswordTextbox.value.trim() === editConfirmPasswordTextbox.value.trim()) {
        const user = {
            userId: parseInt(userId, 10),
            userName: editUserNameTextbox,
            userEmail: editdUserEmailTextbox,
            userPassword: editUserPasswordTextbox
        };

        fetch(uri + `/api/WordQuestUser/${userId}`, {
            method: 'PUT',
            headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        })
        .then(() => getUsers())
        .catch(error => console.error('Unable to update user.', error));

        closeInput();

        return false;
    } else {
        console.error('Passwords do not match.');
    }
}

function closeInput() {
  document.getElementById('editForm').style.display = 'none';
}

function _displayCount(userCount) {
  const name = (userCount === 1) ? 'user' : 'users';

  document.getElementById('counter').innerText = `${userCount} ${name}`;
}

function _displayUsers(data) {
  const tBody = document.getElementById('users');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(user => {
    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${user.userId})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteUser(${user.userId})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    let userNameNode = document.createTextNode(user.userName);
    td1.appendChild(userNameNode);

    let td2 = tr.insertCell(1);
    let userEmailNode = document.createTextNode(user.userEmail);
    td2.appendChild(userEmailNode);

    let td3 = tr.insertCell(2);
    let userPasswordNode = document.createTextNode(user.userPassword);
    td3.appendChild(userPasswordNode);

    let td4 = tr.insertCell(3);
    td4.appendChild(editButton);

    let td5 = tr.insertCell(4);
    td5.appendChild(deleteButton);
  });

  users = data;
}
