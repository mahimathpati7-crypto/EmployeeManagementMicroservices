import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../core/services/employee.service';
import { MemberService } from '../../core/services/member.service';
import { DepartmentService } from '../../core/services/department.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {

  employeeCount = 0;
  memberCount = 0;
  departmentCount = 0;

  constructor(
    private employeeService: EmployeeService,
    private memberService: MemberService,
    private departmentService: DepartmentService
  ) {}

  ngOnInit(): void {

    this.employeeService.getEmployees()
      .subscribe({
        next: (employees) => {
          this.employeeCount = employees.length;
        },
        error: (err) => {
          console.error('Error loading employees', err);
        }
      });

    this.memberService.getMembers()
      .subscribe({
        next: (members) => {
          this.memberCount = members.length;
        },
        error: (err) => {
          console.error('Error loading members', err);
        }
      });

    this.departmentService.getDepartments()
      .subscribe({
        next: (departments) => {
          this.departmentCount = departments.length;
        },
        error: (err) => {
          console.error('Error loading departments', err);
        }
      });
  }
}
