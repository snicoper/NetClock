export abstract class AuditableEntity {
  createdBy: string;
  created: Date;
  lastModifiedBy: string;
  lastModified: Date;
}
