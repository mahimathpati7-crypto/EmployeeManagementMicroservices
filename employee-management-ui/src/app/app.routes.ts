import { Routes } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';

import { EmployeeListComponent } from './features/employees/employee-list/employee-list.component';
import { EmployeeCreateComponent } from './features/employees/employee-create/employee-create.component';
import { EmployeeEditComponent } from './features/employees/employee-edit/employee-edit.component';

import { MemberListComponent } from './features/members/member-list/member-list.component';
import { MemberCreateComponent } from './features/members/member-create/member-create.component';
import { MemberEditComponent } from './features/members/member-edit/member-edit.component';

import { DepartmentListComponent } from './features/departments/department-list/department-list.component';
import { DepartmentCreateComponent } from './features/departments/department-create/department-create.component';
import { DepartmentEditComponent } from './features/departments/department-edit/department-edit.component';

export const routes: Routes = [

  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },

  {
    path: 'dashboard',
    component: DashboardComponent
  },

  {
    path: 'employees',
    component: EmployeeListComponent
  },

  {
    path: 'employees/add',
    component: EmployeeCreateComponent
  },

  {
    path: 'employees/edit/:id',
    component: EmployeeEditComponent
  },

  {
    path: 'members',
    component: MemberListComponent
  },
  {
  path: 'members/add',
  component: MemberCreateComponent
},
  {
  path: 'members/edit/:id',
  component: MemberEditComponent
},

  {
    path: 'departments',
    component: DepartmentListComponent
  },
  {
  path: 'departments/add',
  component: DepartmentCreateComponent
},
{
  path: 'departments/edit/:id',
  component: DepartmentEditComponent
}
  

];