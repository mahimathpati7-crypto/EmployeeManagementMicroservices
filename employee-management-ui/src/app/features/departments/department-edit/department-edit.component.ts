
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { DepartmentService } from '../../../core/services/department.service';
import { EmployeeService } from '../../../core/services/employee.service';

@Component({
  selector: 'app-department-edit',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './department-edit.component.html',
  styleUrl: './department-edit.component.scss'
})
export class DepartmentEditComponent implements OnInit {

  employees: any[] = [];

  department: any = {
    departmentId: 0,
    departmentName: '',
    departmentCode: '',
    description: '',
    location: '',
    managerId: 0,
    isActive: true
  };

  constructor(
    private departmentService: DepartmentService,
    private employeeService: EmployeeService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {

    const id = Number(
      this.route.snapshot.paramMap.get('id')
    );

    // Load Employee List
    this.loadEmployees();

    // Load Department Details
    this.loadDepartment(id);

  }

  // Get Employees
  loadEmployees(): void {

    this.employeeService.getEmployees()
      .subscribe({

        next: (data: any) => {

          console.log('Employee API Response:', data);

          this.employees = data;

          console.log('Employee List:', this.employees);

        },

        error: (err) => {

          console.error('Employee API Error:', err);

        }

      });

  }

  // Get Department By ID
  loadDepartment(id: number): void {

    this.departmentService.getDepartmentById(id)
      .subscribe({

        next: (data: any) => {

          console.log('Department API Response:', data);

          this.department = {
            ...data,
            managerId: Number(data.managerId)
          };

        },

        error: (err) => {

          console.error('Department API Error:', err);

        }

      });

  }

  // Update Department
  updateDepartment(): void {

    this.departmentService
      .updateDepartment(
        this.department.departmentId,
        this.department
      )
      .subscribe({

        next: () => {

          alert('Department Updated Successfully');

          this.router.navigate(['/departments']);

        },

        error: (err) => {

          console.error('Update Error:', err);

          alert('Failed to Update Department');

        }

      });

  }

}
