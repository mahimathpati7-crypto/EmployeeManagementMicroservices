import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { EmployeeService } from '../../../core/services/employee.service';
import { DepartmentService } from '../../../core/services/department.service';
import { MemberService } from '../../../core/services/member.service';

@Component({
  selector: 'app-employee-create',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './employee-create.component.html',
  styleUrl: './employee-create.component.scss'
})
export class EmployeeCreateComponent implements OnInit {

  employee = {
    employeeId: 0,
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    gender: '',
    dateOfBirth: '',
    joiningDate: '',
    salary: 0,
    departmentId: 0,
    memberId: 0,
    isActive: true
  };

  departments: any[] = [];
  members: any[] = [];

  constructor(
    private employeeService: EmployeeService,
    private departmentService: DepartmentService,
    private memberService: MemberService,
    private router: Router
  ) {}

  ngOnInit(): void {

    this.departmentService.getDepartments()
      .subscribe(data => {
        this.departments = data;
      });

    this.memberService.getMembers()
      .subscribe(data => {
        this.members = data;
      });
  }

  saveEmployee(): void {

    this.employeeService.createEmployee(this.employee as any)
      .subscribe({
        next: () => {
          alert('Employee Added Successfully');
          this.router.navigate(['/employees']);
        },
        error: (error) => {
          console.error(error);
          alert('Failed to Add Employee');
        }
      });
  }
}