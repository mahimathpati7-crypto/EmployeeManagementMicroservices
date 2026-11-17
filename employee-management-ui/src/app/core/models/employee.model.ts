export interface Employee {
  employeeId?: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  gender: string;
  dateOfBirth: string;
  joiningDate: string;
  salary: number;
  departmentId: number;
  memberId: number;
  isActive: boolean;
  createdDate?: string;
  updatedDate?: string;
}