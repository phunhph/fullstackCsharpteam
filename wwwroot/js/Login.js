const box = document.querySelector(".form-box");
const login = document.querySelector(".register-link");
const register = document.querySelector(".login-link");

register.addEventListener("click", () => {
    box.classList.add("active");
});

login.addEventListener("click", () => {
    box.classList.remove("active");
});
// show password
document.getElementById("clock").onclick = function () {
    console.log(document.getElementById("clock").name);
    if (document.getElementById("clock").name == "lock-closed-outline") {
        document.getElementById("clock").name = "lock-open-outline";
        document.getElementById("pass").type = "text";
    } else {
        document.getElementById("clock").name = "lock-closed-outline";
        document.getElementById("pass").type = "password";
    }
};