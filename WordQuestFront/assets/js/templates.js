document.addEventListener('DOMContentLoaded', () => {
    // Header
    fetch('templates.html')
        .then(response => response.text())
        .then(html => {
            const parser = new DOMParser();
            const doc = parser.parseFromString(html, 'text/html');
            const navTemplate = doc.getElementById('header-template');
            if (navTemplate) {
                const navClone = navTemplate.content.cloneNode(true);
                document.body.insertBefore(navClone, document.body.firstChild);
            }
        });

    // Footer
    fetch('templates.html')
        .then(response => response.text())
        .then(html => {
            const parser = new DOMParser();
            const doc = parser.parseFromString(html, 'text/html');
            const footerTemplate = doc.getElementById('footer-template');
            if (footerTemplate) {
                const footerClone = footerTemplate.content.cloneNode(true);
                document.body.appendChild(footerClone);
            }
        });
    
    // SignIn
    fetch('templates.html')
    .then(response => response.text())
    .then(html => {
        const parser = new DOMParser();
        const doc = parser.parseFromString(html, 'text/html');
        const signInTemplate = doc.getElementById('signin-template');
        if (signInTemplate) {
            const signInClone = signInTemplate.content.cloneNode(true);
            document.body.appendChild(signInClone);

            const signInOverlay = document.getElementById('signin-overlay');
            //const signInOverlayContent = signInOverlay.querySelector('.overlay-content');

            document.getElementById('OpenSignIn').addEventListener('click', function() {
                signInOverlay.style.display = 'flex';
            });

            // Hide the overlay when clicking outside of the overlay content
            window.addEventListener('click', function(event) {
                if (event.target === signInOverlay) {
                    signInOverlay.style.display = 'none';
                }
            });
        }
    });

    // LogIn
    fetch('templates.html')
    .then(response => response.text())
    .then(html => {
        const parser = new DOMParser();
        const doc = parser.parseFromString(html, 'text/html');
        const logInTemplate = doc.getElementById('login-template');
        if (logInTemplate) {
            const logInClone = logInTemplate.content.cloneNode(true);
            document.body.appendChild(logInClone);

            const logInOverlay = document.getElementById('login-overlay');
            //const logInOverlayContent = logInOverlay.querySelector('.overlay-content');

            document.getElementById('OpenLogIn').addEventListener('click', function() {
                logInOverlay.style.display = 'flex';
            });

            // Hide the overlay when clicking outside of the overlay content
            window.addEventListener('click', function(event) {
                if (event.target === logInOverlay) {
                    logInOverlay.style.display = 'none';
                }
            });
        }
    });
});



