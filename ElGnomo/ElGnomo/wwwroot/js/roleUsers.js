const RoleChanged = (URL) => {
    $.post(URL, {
        "RoleId": $("#RoleId option:selected").val()
    }, (role) => {
        document.getElementById("Role_Description").value = role.description;
    });
}

const UserChanged = (URL) => {
    $.post(URL, {
        "UserId": $("#UserId option:selected").val()
    }, (user) => {
        document.getElementById("User_FirstName").value = user.firstName;
        document.getElementById("User_LastName").value = user.lastName;
    });
}