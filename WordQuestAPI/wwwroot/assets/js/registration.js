async function register() {
    
    const username = document.getElementById('signin-username').value;
    const email = document.getElementById('email').value;
    const phone = document.getElementById('phone').value;
    const password = document.getElementById('signin-password').value;
    const confirmPassword = document.getElementById('confirm-password').value;

    // Vérifier si les mots de passe correspondent
    if (password !== confirmPassword) {
        console.error('Passwords do not match.');
        return;
    }

    // Créer un objet user avec les données du formulaire
    const registerModel = {
        UserName: username,
        Password: password,
        Email: email,
        PhoneNumber: phone
    };

    const loginModel = {
        Username: username,
        Password: password
    }

    try {
        // Effectuer une requête AJAX vers l'endpoint d'inscription
        const response = await fetch('../../../account/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(registerModel)
        });

        if (response.ok) {
            console.log('User registered successfully.');
            loginUser(loginModel);
        } else {
            console.error('Failed to register user:', response.statusText);
        }
    } catch (error) {
        console.error('Error during registration:', error);
    }
}

async function login() {

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    const loginModel = {
        Username: username,
        Password: password
    }

    try {
        loginUser(loginModel);

    } catch (error) {
        console.error('Error during login:', error);
    }
}

async function logout() {
    const response = await fetch('../../../account/logout', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify()
    });
    if (response.ok) {
        console.log('LogOut successfully.');
        window.location.href = '/';
    } else {
        console.error('Failed to logout user :', response.statusText);
    }
}

async function loginUser(loginModel) {
    const response = await fetch('../../../account/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(loginModel)
    });
    if (response.ok) {
        console.log('Login successfully.');
        window.location.href = '/';
    } else {
        console.error('Failed to login user :', response.statusText);
    }
}

async function checkWhoIsLogIn() {
    const response = await fetch('../../../account/check-login-status', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify()
    });
    const data = await response.json();
    if (data.isLoggedIn) {
        return data.id;
    } else {
        return null;
    }
}

async function showLogIn(){
    id = await checkWhoIsLogIn();
    if (id) {
        const user = await getUser(id);
        document.getElementById('connection-menu').classList = ["has-submenu hidden"];
        document.getElementById('user-menu').classList = ["has-submenu"];
        document.getElementById('user-menu').querySelector('a').innerText = user.userName;
    }
}

// Ajouter un écouteur d'événement pour la soumission du formulaire
//document.getElementById('signin-template').addEventListener('submit', addUser);
const signinForm = document.getElementById('signin-template');
if (signinForm) {
    signinForm.addEventListener('submit', addUser);
}