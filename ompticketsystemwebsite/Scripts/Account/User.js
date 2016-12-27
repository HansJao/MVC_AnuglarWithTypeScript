/** 使用者基本資料型別 */
var User = (function () {
    function User() {
    }
    /** 姓名 */
    User.prototype.fullName = function () {
        return this.firstName + " " + this.lastName;
    };
    return User;
}());
//# sourceMappingURL=User.js.map