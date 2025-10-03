type Role = "Admin" | "User" | "Guest";

interface IUser {
  id: number;
  name: string;
  email: string;
  role: Role;
}

class UserManager {
  private users: IUser[] = [];
  private nextId: number = 1;

  public addUser(name: string, email: string, role: Role): void {
    const newUser: IUser = {
      id: this.nextId++,
      name,
      email,
      role
    };
    this.users.push(newUser);
    this.renderUsers();
    
  }

  public deleteUser(id: number): void {
    this.users = this.users.filter(user => user.id !== id);
    this.renderUsers();
  }

  public updateUserRole(id: number, newRole: Role): void {
    const user = this.findUserBy("id", id);
    if (user) {
      user.role = newRole;
      this.renderUsers();
    }
  }

  public findUserBy<K extends keyof IUser>(key: K, value: IUser[K]): IUser | undefined {
    return this.users.find(user => user[key] === value);
  }

  
  private renderUsers(): void {
    const userList = document.getElementById("userList") as HTMLTableSectionElement;
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


(window as any).adduser = () => {
  const nameInput = (document.getElementById("name") as HTMLInputElement).value;
  const emailInput = (document.getElementById("email") as HTMLInputElement).value;
  const roleInput = (document.getElementById("role") as HTMLSelectElement).value as Role;

  if(nameInput == "" || emailInput==""){
    alert("enter detials")
  }

  if (nameInput && emailInput) {
    userManager.addUser(nameInput, emailInput, roleInput);
    (document.getElementById("name") as HTMLInputElement).value='';
    (document.getElementById("email") as HTMLInputElement).value='';
    (document.getElementById("role") as HTMLSelectElement).value  ='';
  }
};

(window as any).deleteUser = (id: number) => {
  userManager.deleteUser(id);
};

(window as any).editUser = (id: number) => {
  const newRole = prompt("Enter new role (Admin, User, Guest):") as Role;
  if (newRole === "Admin" || newRole === "User" || newRole === "Guest") {
    userManager.updateUserRole(id, newRole);
  } else {
    alert("Invalid role!");
  }
};

