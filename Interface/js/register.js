window.onload = async function () {
    document.getElementById("register-btn").addEventListener("click", async function () {
        const username = document.getElementById("username").value;
        const email = document.getElementById("email").value;
        const password = document.getElementById("password").value;
        const passwordR = document.getElementById("password-repeat").value;

        if (password !== passwordR) {
            alert("The password repeat is different from the password");
            return;
        }

        const resp = await fetchData(`${apiUrl}/auth/register`, 'POST', null, {
            username: username,
            email: email,
            password: password
        });

        if (resp.statusCode == 200) {
            localStorage.setItem("jwt", resp.token);
            window.location.replace("table.html");
        }

        else alert(resp);
    });
}