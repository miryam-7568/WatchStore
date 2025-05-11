
document.addEventListener("DOMContentLoaded", () => {
    const userData = JSON.parse(localStorage.getItem('user'));

    if (userData) {
        document.getElementById('welcomeMessage').textContent = `שלום ${userData.userName}, התחברת בהצלחה, מיד נצלול פנימה.`;
        document.getElementById('userName').value = userData.userName.trim();
        document.getElementById('firstName').value = userData.firstName.trim();
        document.getElementById('lastName').value = userData.lastName.trim();
        document.getElementById('password').value = userData.password.trim();
    } else {
        window.location.href = "UserDetails.html";
    }

    document.getElementById('updateForm').addEventListener('submit', update);
});

const update = async (event) => {
    event.preventDefault(); 

    try {
        const userData = JSON.parse(localStorage.getItem('user'));

        const updatedUserData = {
            userId: userData.userId,
            userName: document.getElementById('userName').value.trim(),
            firstName: document.getElementById('firstName').value.trim(),
            lastName: document.getElementById('lastName').value.trim(),
            Password: document.getElementById('password').value.trim()
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
