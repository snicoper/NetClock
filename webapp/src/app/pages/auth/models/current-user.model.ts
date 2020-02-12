/** Modelo actual logueado, datos que almacena en localStorage. */
export class CurrentUserModel {
  public id: string;
  public userName: string;
  public firstName: string;
  public lastName: string;
  public fullName: string;
  public email: string;
  public token: string;
  public expires: Date;
}
