
function addUser() {
    
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
    const user = {
        username: username,
        email: email,
        phone: phone
    };
    const registerModel = {
        User: user,
        Password: password
    };

    try {
        // Effectuer une requête AJAX vers l'endpoint d'inscription
        const response = fetch('../../../api/account/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(registerModel)
        });

        if (response.ok) {
            // Inscription réussie, rediriger l'utilisateur vers une nouvelle page ou afficher un message de réussite
            console.log('User registered successfully.');
            // Redirection vers une nouvelle page
            window.location.href = '/dashboard'; // Adapté à votre application
        } else {
            // Afficher un message d'erreur en cas d'échec de l'inscription
            console.error('Failed to register user:', response.statusText);
        }
    } catch (error) {
        console.error('Error during registration:', error);
    }
}

// Ajouter un écouteur d'événement pour la soumission du formulaire
//document.getElementById('signin-template').addEventListener('submit', addUser);
const signinForm = document.getElementById('signin-template');
if (signinForm) {
    signinForm.addEventListener('submit', addUser);
}