showRegister = () => {
    const div = document.querySelector("#register");
    div.setAttribute("style", "visibility:visible");
}
login = async () => {
    const username = document.querySelector("#name").value;
    const password = document.querySelector("#password").value;
    if (!username || !password) {
        alert("username and password are required");
    }
    const loginrequest = {
        UserName: username,
        Password: password
    }
    try {
        const response = await fetch("api/Users/login", {
            method: "POST",
            headers: {
                "content-type": "application/json"
            },
            "body": JSON.stringify(loginrequest)
        })
        if (response.ok) {
            const usertolocalstorage = await response.json();
            localStorage.setItem("user", JSON.stringify(usertolocalstorage))
            window.location.href = "UserDetails.html";
        }
        else {
            switch (response.status) {
                case 400:
                    const badRequestData = await response.json();
                    alert(`Bad request: ${badRequestData.message || 'Invalid input. Please check your data.'}`);
                    break;
                case 401:
                    alert("Unauthorized: Please check your credentials.");
                    break;
                case 404:
                    alert("User not found. Please check your user name and password.");
                    break;
                case 500:
                    alert("Server error. Please try again later.");
                    break;
                default:
                    alert(`Unexpected error: ${response.status}`);
            }
        }
    }
    catch (error) {
        alert("Error: " + error.message);
    }
}
register = async () => {
    const username = document.querySelector("#rname").value;
    const password = document.querySelector("#rpassword").value;
    const firstname = document.querySelector("#firstname").value;
    const lastname = document.querySelector("#lastname").value;
    
    if (!username || !password) {
        alert("username and password are required");
    }
    const registerrequest = {
        UserName: username,
        Password: password,
        FirstName: firstname,
        LastName: lastname
    }
    try {
        const response = await fetch("api/Users/register", {
            method: "POST",
            headers: {
                "content-type": "application/json",
                
            },
            "body": JSON.stringify(registerrequest)
        })
        const data = await response.json(); 
        if (response.ok) {
            localStorage.setItem("user", JSON.stringify(data))
            alert("user registered successfully")
        }
        else {
            switch (response.status) {
                case 400:
                    alert(`Bad request: ${data.message || 'Invalid input. Please check your data.'}`);
                    break;
                case 401:
                    alert( `Unauthorized:${data.message || ' Please check your credentials.'}`);
                    break;
                case 500:
                    alert("Server error. Please try again later.");
                    break;
                default:
                    alert(`Unexpected error: ${response.status + data.message}`);
            }
        }
    }
    catch (error) {
        alert("Error: " + error.message);
    }
}
