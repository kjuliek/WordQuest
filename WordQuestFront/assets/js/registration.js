const uri = 'http://127.0.0.1:5094';

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
        const response = await fetch(uri + '/account/register', {
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
    try {
        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;

        const loginModel = {
            Username: username,
            Password: password
        };

        await loginUser(loginModel); // Attendre que loginUser se termine

    } catch (error) {
        console.error('Error during login:', error);
    }
}

async function logout() {
    try {
        const response = await fetch(uri + '/account/logout', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify()
        });
        
        if (response.ok) {
            console.log('LogOut successfully.');
            localStorage.removeItem('token');
            showLogIn();
        } else {
            console.error('Failed to logout user :', response.statusText);
        }
    } catch (error) {
        console.error('Error during logout:', error);
    }
}

async function loginUser(loginModel) {
    try {
        const response = await fetch(uri + '/account/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginModel)
        });

        if (response.ok) {
            const data = await response.json();
            console.log('Login successfully.');
            localStorage.setItem('token', data.token);
            showLogIn();
        } else {
            console.error('Failed to login user :', response.statusText);
        }
    } catch (error) {
        console.error('Error during login:', error);
    }
}

async function checkWhoIsLogIn() {
    try {
        const response = await fetch(uri + '/account/check-login-status', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
        if (response.ok) {
            const data = await response.json();
            if (data.isLoggedIn) {
                user = await getUserByName(data.id);
                return user;
            } else {
                return null;
            }
        } else {
            console.error('Failed to check login status:', response.statusText);
            return null;
        }
    } catch (error) {
        console.error('Error checking login status:', error);
        return null;
    }
}

async function showLogIn(){
    user = await checkWhoIsLogIn();
    if (user) {
        document.getElementById('connection-menu').classList = ["has-submenu hidden"];
        document.getElementById('user-menu').classList = ["has-submenu"];
        document.getElementById('user-menu').querySelector('a').innerText = user.userName;
    } else {
        document.getElementById('connection-menu').classList = ["has-submenu"];
        document.getElementById('user-menu').classList = ["has-submenu hidden"];
    }
}

// Ajouter un écouteur d'événement pour la soumission du formulaire
//document.getElementById('signin-template').addEventListener('submit', addUser);
const signinForm = document.getElementById('signin-template');
if (signinForm) {
    signinForm.addEventListener('submit', addUser);
}