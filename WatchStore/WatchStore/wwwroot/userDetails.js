
document.addEventListener("DOMContentLoaded", () => {
    const userData = JSON.parse(localStorage.getItem('user'));

    if (userData) {
        document.getElementById('welcomeMessage').textContent = `שלום ${userData.firstName}, התחברת בהצלחה, מיד נצלול פנימה.`;
        document.getElementById('userName').value = userData.userName;
        document.getElementById('firstName').value = userData.firstName;
        document.getElementById('lastName').value = userData.lastName;
        document.getElementById('password').value = userData.password;
    } else {
        window.location.href = "UserDetails.html";
    }

    document.getElementById('updateForm').addEventListener('submit', update);
});

const update = async (event) => {
    event.preventDefault(); // ⛔ prevent page refresh on submit

    try {
        const userData = JSON.parse(localStorage.getItem('user'));

        const updatedUserData = {
            userId: userData.userId,
            userName: document.getElementById('userName').value,
            firstName: document.getElementById('firstName').value,
            lastName: document.getElementById('lastName').value,
            Password: document.getElementById('password').value
        };

        const response = await fetch(`/api/users/${userData.userId}`, {
            method: "PUT",
            headers: {
                "content-type": "application/json"
            },
            body: JSON.stringify(updatedUserData)
        });

        if (response.ok) {
            alert("Details updated successfully");
            document.getElementById('welcomeMessage').textContent = `שלום ${updatedUserData.firstName}, התחברת בהצלחה, מיד נצלול פנימה.`;
            document.getElementById('password').value = '*'.repeat(updatedUserData.Password.length);
            localStorage.setItem('user', JSON.stringify(updatedUserData));
        } else {
            alert('שגיאה בעדכון הפרטים. אנא נסה שוב.');
        }
    } catch (error) {
        console.error(error);
        window.location.href = "UserDetails.html";
    }
};
