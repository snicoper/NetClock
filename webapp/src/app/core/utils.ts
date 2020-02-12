export abstract class Utils {
  /** Pasar bytes a una medida legible según el size. */
  public static formatSizeUnit(value: number): string {
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
    if (value === 0) {
      return '0 Byte';
    }

    const i = parseInt(String(Math.floor(Math.log(value) / Math.log(1024))), 10);

    return `${Math.round(value / Math.pow(1024, i))} ${sizes[i]}`;
  }
}
