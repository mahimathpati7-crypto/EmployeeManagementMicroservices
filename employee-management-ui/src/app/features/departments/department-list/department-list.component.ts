
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { DepartmentService } from '../../../core/services/department.service';
import { EmployeeService } from '../../../core/services/employee.service';
import { Department } from '../../../core/models/department.model';

@Component({
  selector: 'app-department-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './department-list.component.html',
  styleUrl: './department-list.component.scss'
})
export class DepartmentListComponent implements OnInit {

  departments: Department[] = [];

  employees: any[] = [];

  constructor(
    private departmentService: DepartmentService,
    private employeeService: EmployeeService
  ) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  // Load Employees
  loadEmployees(): void {

    this.employeeService.getEmployees()
      .subscribe({

        next: (data: any) => {

          this.employees = data;

          console.log('Employees:', this.employees);

          // Load Departments after Employees
          this.loadDepartments();

        },

        error: (err) => {
          console.error('Employee API Error:', err);
        }

      });

  }

  // Load Departments
  loadDepartments(): void {

    this.departmentService
      .getDepartments()
      .subscribe({

        next: (data) => {
          this.departments = data;
        },

        error: (err) => {
          console.error('Department API Error:', err);
        }

      });

  }

  // Get Manager Name using EmployeeId
  getManagerName(managerId: number): string {

    const employee = this.employees.find(
      x => Number(x.employeeId) === Number(managerId)
    );

    return employee
      ? `${employee.firstName} ${employee.lastName}`
      : 'Not Assigned';

  }

  // Delete Department
  deleteDepartment(id: number): void {

    if (confirm('Are you sure you want to delete this Department?')) {

      this.departmentService.deleteDepartment(id)
        .subscribe(() => {

          alert('Department Deleted Successfully');

          this.loadDepartments();

        });

    }

  }

}
