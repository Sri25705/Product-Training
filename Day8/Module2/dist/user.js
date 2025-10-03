"use strict";
class UserManager {
    constructor() {
        this.users = [];
        this.nextId = 1;
    }
    addUser(name, email, role) {
        const newUser = {
            id: this.nextId++,
            name,
            email,
            role
        };
        this.users.push(newUser);
        this.renderUsers();
    }
    deleteUser(id) {
        this.users = this.users.filter(user => user.id !== id);
        this.renderUsers();
    }
    updateUserRole(id, newRole) {
        const user = this.findUserBy("id", id);
        if (user) {
            user.role = newRole;
            this.renderUsers();
        }
    }
    findUserBy(key, value) {
        return this.users.find(user => user[key] === value);
    }
    renderUsers() {
        const userList = document.getElementById("userList");
        userList.innerHTML = "";
        this.users.forEach(user => {
            const row = document.createElement("tr");
            row.innerHTML = `
        <td>${user.id}</td>
        <td>${user.name}</td>
        <td>${user.email}</td>
        <td>${user.role}</td>
        <td>
          <button onclick="editUser(${user.id})">Edit</button>
          <button onclick="deleteUser(${user.id})">Delete</button>
        </td>
      `;
            userList.appendChild(row);
        });
    }
}
const userManager = new UserManager();
window.adduser = () => {
    const nameInput = document.getElementById("name").value;
    const emailInput = document.getElementById("email").value;
    const roleInput = document.getElementById("role").value;
    if (nameInput == "" || emailInput == "") {
        alert("enter detials");
    }
    if (nameInput && emailInput) {
        userManager.addUser(nameInput, emailInput, roleInput);
        document.getElementById("name").value = '';
        document.getElementById("email").value = '';
        document.getElementById("role").value = '';
    }
};
window.deleteUser = (id) => {
    userManager.deleteUser(id);
};
window.editUser = (id) => {
    const newRole = prompt("Enter new role (Admin, User, Guest):");
    if (newRole === "Admin" || newRole === "User" || newRole === "Guest") {
        userManager.updateUserRole(id, newRole);
    }
    else {
        alert("Invalid role!");
    }
};
