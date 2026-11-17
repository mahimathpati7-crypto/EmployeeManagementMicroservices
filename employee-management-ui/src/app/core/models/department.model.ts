export interface Department {
  departmentId: number;
  departmentName: string;
  departmentCode: string;
  description: string;
  location: string;
  managerId: number;
  isActive: boolean;
  createdDate?: string;
  updatedDate?: string;
}