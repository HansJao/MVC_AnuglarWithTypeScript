/** 使用者基本資料型別 */
class User {
    /** 名 */
    firstName: string;
    /** 姓 */
    lastName: string;
    /** 姓名 */
    fullName(): string {
        return this.firstName + " " + this.lastName;
    }
}