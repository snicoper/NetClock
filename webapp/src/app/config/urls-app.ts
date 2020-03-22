/** URLs en la APP. */
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
