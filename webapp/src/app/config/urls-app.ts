/**
 * URLs en la APP.
 *
 * Importante, en los HTMLs, poner siempre comentario con el valor usado por si se ha de remplazar.
 * <!-- '/admin/accounts/{slug}/details' -->
 */
export const UrlsApp = {
  /** Home. */
  home: '/',

  /** Auth. */
  login: '/auth/login',
  logout: '/auth/logout',
  recoveryPassword: '/auth/recovery-password',
  recoveryPasswordSuccess: '/auth/recovery-password/success',

  /** Accounts. */
  accounts: '/accounts/profile',
  changePassword: '/accounts/change-password',

  /** Admin. */
  admin: '/admin',
  adminUserList: '/admin/accounts',
  adminUserDetails: '/admin/accounts/{slug}/details',
  adminUserUpdate: '/admin/accounts/{slug}/update',
  adminUserCreate: '/admin/accounts/create',

  /**
   * Utiliza una de las propiedades de UrlsApp para remplazar {algo} por valor en los args.
   * @param url Una de las propiedades.
   * @param args Remplaza el {key} por el value de.
   */
  replace(url: string, args: object): string {
    const keys = Object.keys(args);
    const values = Object.values(args);

    for (let i = 0; i < keys.length; i += 1) {
      const key = '{' + keys[i] + '}';
      const value = values[i];
      url = url.replace(key, value);
    }

    return url;
  }
};
