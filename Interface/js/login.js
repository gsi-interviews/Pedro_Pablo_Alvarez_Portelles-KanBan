window.onload = async function () {
    document.getElementById("login-btn").addEventListener("click", async function () {
        const username = document.getElementById("username").value;
        const password = document.getElementById("password").value;

        const resp = await fetchData(`${apiUrl}/auth/login`, 'POST', null, {
            username: username,
            password: password
        });

        if (resp.statusCode == 200) {
            localStorage.setItem("jwt", resp.token);
            window.location.replace("table.html");
        }

        else alert(resp);
    });
}