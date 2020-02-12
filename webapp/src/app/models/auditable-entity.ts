export abstract class AuditableEntity {
  public createdBy: string;
  public created: Date;
  public lastModifiedBy: string;
  public lastModified: Date;
}
