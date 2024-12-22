// common.js
function getCookieValue(name) {
    // Create a regular expression to match the cookie name
    const nameEQ = name + "=";
    const cookies = document.cookie.split(';');

    for (let i = 0; i < cookies.length; i++) {
        let cookie = cookies[i];
        // Remove leading spaces
        while (cookie.charAt(0) === ' ') {
            cookie = cookie.substring(1, cookie.length);
        }
        // Check if the cookie name matches
        if (cookie.indexOf(nameEQ) === 0) {
            return cookie.substring(nameEQ.length, cookie.length);
        }
    }
    return null;
}

function getCSRF() {
    let csrf = getCookieValue('_csrf');
    return csrf;
}

function getAllCookies() {
    return document.cookie;
}

async function sendRequestWithCookies(url, method, body) {
    try {
        const csrfToken = getCSRF();

        const requestOptions = {
            method: method,
            credentials: 'include', // Ensures cookies are included
            headers: {
                'Content-Type': 'application/json',
                'X-Csrf-Token': csrfToken
            }
        };

        if (body) {
            requestOptions.body = JSON.stringify(body);
        }

        const response = await fetch(url, requestOptions);

        let wrappedResponse = {
            statusCode: response.status
        };
        wrappedResponse.response = await response.json();


        return wrappedResponse;
    } catch (error) {
        let wrappedResponse = {
            status: 500,
            error: error
        };

        console.error('Error sending request: ', error);
        return wrappedResponse;
    }
}
window.setFocus = (element) => {
    element.focus();
};
// Function to get the list of last used identities
function fetchLoginRecords() {
    const identities = document.cookie
        .split('; ')
        .find(row => row.startsWith('lastUsedIdentities='));

    return identities ? JSON.parse(decodeURIComponent(identities.split('=')[1])) : [];
}


// Function to store login record
function storeLoginRecord(identity) {
    let identities = fetchLoginRecords();

    // Convert the new identity's email to lowercase
    const newEmail = identity.email.toLowerCase();

    // Remove the identity if it already exists (comparing by email in lowercase)
    identities = identities.filter(id => id.email.toLowerCase() !== newEmail);

    // Add the new identity to the beginning of the list
    identities.unshift({ ...identity, email: newEmail });

    // Limit the list to 10 entries
    identities = identities.slice(0, 10);

    // Save the updated list to the cookie
    document.cookie = `lastUsedIdentities=${encodeURIComponent(JSON.stringify(identities))}; max-age=31536000; path=/`;
}

// Function to display the last used identities
function displayLastUsedIdentities() {
    const identities = fetchLoginRecords();
    console.log('Last used identities:', identities);
    // You can implement your own UI to display these identities
}

// Example usage
/*
const newIdentity = {
    email: "user@example.com",
    fullName: "User Doe"
};
storeLoginRecord(newIdentity);
displayLastUsedIdentities();
*/