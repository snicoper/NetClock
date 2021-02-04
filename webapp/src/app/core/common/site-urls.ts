/**
 * URLs en la APP.
 *
 * Importante, en los HTMLs, poner siempre comentario con el valor usado por si se ha de remplazar.
 * <!-- '/admin/accounts/{slug}/details' -->
 */
export const siteUrls = {
  /** Home. */
  home: '/',

  /** Auth. */
  authLogin: '/auth/login',
  authLogout: '/auth/logout',
  authRecoveryPassword: '/auth/recovery-password',
  authRecoveryPasswordSuccess: '/auth/recovery-password/success',

  /** Accounts. */
  accountsProfile: '/accounts/profile',
  accountsChangePassword: '/accounts/change-password',

  /** Admin. */
  admin: '/admin',
  adminAccounts: '/admin/accounts',
  adminAccountsDetails: '/admin/accounts/{slug}/details',
  adminAccountsUpdate: '/admin/accounts/{slug}/update',
  adminAccountsCreate: '/admin/accounts/create',
  adminAccountsChangePassword: '/admin/accounts/{slug}/change-password',

  /** Errors. */
  errorsForbidden: '/errors/403',

  /**
   * Utiliza una de las propiedades de SiteUrls para remplazar {algo} por valor en los args.
   *
   * @param url Una de las propiedades.
   * @param args Remplaza el {key} por el value de.
   */
  replace: (url: string, args: Record<string, string>) => {
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
