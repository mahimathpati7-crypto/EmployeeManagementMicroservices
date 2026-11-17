import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { DepartmentService } from '../../../core/services/department.service';
import { EmployeeService } from '../../../core/services/employee.service';

@Component({
  selector: 'app-department-create',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './department-create.component.html',
  styleUrl: './department-create.component.scss'
})
export class DepartmentCreateComponent implements OnInit {

  department: any = {
    departmentName: '',
    departmentCode: '',
    description: '',
    location: '',
    managerId: 0,
    isActive: true
  };

  employees: any[] = [];

  constructor(
    private departmentService: DepartmentService,
    private employeeService: EmployeeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {

    this.employeeService.getEmployees().subscribe({

      next: (data: any) => {

        console.log('Employee API Response:', data);

        this.employees = data;

        console.log('Employees Array:', this.employees);

      },

      error: (err) => {

        console.error('Employee API Error:', err);

      }

    });

  }

  saveDepartment(): void {

    console.log('Department Data:', this.department);

    this.departmentService.createDepartment(this.department)
      .subscribe({

        next: () => {

          alert('Department Added Successfully');

          this.router.navigate(['/departments']);

        },

        error: (err) => {

          console.error(err);

          alert('Failed to Add Department');

        }

      });

  }

}