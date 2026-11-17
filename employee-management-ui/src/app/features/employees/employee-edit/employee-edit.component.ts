import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { EmployeeService } from '../../../core/services/employee.service';
import { DepartmentService } from '../../../core/services/department.service';
import { MemberService } from '../../../core/services/member.service';

import { Employee } from '../../../core/models/employee.model';

@Component({
  selector: 'app-employee-edit',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './employee-edit.component.html',
  styleUrl: './employee-edit.component.scss'
})
export class EmployeeEditComponent implements OnInit {

  employeeId: number = 0;
  employee!: Employee;

  departments: any[] = [];
  members: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private departmentService: DepartmentService,
    private memberService: MemberService,
    private router: Router
  ) {}

  ngOnInit(): void {

    this.employeeId = Number(
      this.route.snapshot.paramMap.get('id')
    );

    this.departmentService.getDepartments()
      .subscribe(data => {
        this.departments = data;
      });

    this.memberService.getMembers()
      .subscribe(data => {
        this.members = data;
      });

    this.loadEmployee();
  }

  loadEmployee(): void {

    this.employeeService
      .getEmployeeById(this.employeeId)
      .subscribe({
        next: (response) => {

          response.dateOfBirth =
            response.dateOfBirth?.split('T')[0];

          response.joiningDate =
            response.joiningDate?.split('T')[0];

          this.employee = response;
        },
        error: (error) => {
          console.error(error);
        }
      });
  }

  updateEmployee(): void {

    this.employeeService
      .updateEmployee(
        this.employee.employeeId!,
        this.employee
      )
      .subscribe({
        next: () => {

          alert('Employee Updated Successfully');

          this.router.navigate(['/employees']);
        },
        error: (error) => {

          console.error(error);

          alert('Failed to Update Employee');
        }
      });
  }
}