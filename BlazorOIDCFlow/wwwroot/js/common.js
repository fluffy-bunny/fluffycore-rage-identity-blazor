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

        const response = await fetch(url, {
            method: method,
            credentials: 'include', // This ensures cookies are included
            headers: {
                'Content-Type': 'application/json',
                'X-Csrf-Token': csrfToken
            },
            body: JSON.stringify(body)
        });

        let wrappedResponse = {
            statusCode: response.status,
        };
        if (response.ok) {
            wrappedResponse.response = await response.json();
            return wrappedResponse;
        } else {
            return wrappedResponse;
        }
    } catch (error) {
        let wrappedResponse = {
            status: 500,
            error: error
        };
        console.error('Error    sending request: ', error);
        return wrappedResponse
    }
}