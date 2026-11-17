import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { EmployeeService } from '../../../core/services/employee.service';
import { Employee } from '../../../core/models/employee.model';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css'
})
export class EmployeeListComponent implements OnInit {

  employees: Employee[] = [];
  loading = false;
  errorMessage = '';

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.loading = true;
    this.errorMessage = '';

    this.employeeService.getEmployees().subscribe({
      next: (response) => {
        this.employees = response;
        this.loading = false;
      },
      error: (error) => {
        console.error('Employee API Error:', error);
        this.errorMessage = 'Unable to load employees. Please check API.';
        this.loading = false;
      }
    });
  }
  deleteEmployee(id: number): void {

  if (!confirm('Are you sure you want to delete this employee?')) {
    return;
  }

  this.employeeService
    .deleteEmployee(id)
    .subscribe({
      next: () => {

        alert('Employee Deleted Successfully');

        this.loadEmployees();
      },
      error: (error) => {
        console.error(error);
        alert('Failed to Delete Employee');
      }
    });
}
}