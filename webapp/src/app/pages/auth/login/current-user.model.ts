/** Modelo actual logueado, datos que almacena en localStorage. */
export class CurrentUserModel {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  fullName: string;
  email: string;
  token: string;
  expires: Date;
}
