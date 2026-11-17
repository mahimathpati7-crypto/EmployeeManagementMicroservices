import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { MemberService } from '../../../core/services/member.service';

@Component({
  selector: 'app-member-create',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './member-create.component.html',
  styleUrl: './member-create.component.scss'
})
export class MemberCreateComponent {

  member: any = {
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    address: '',
    city: '',
    state: '',
    country: '',
    membershipType: 'Basic',
    membershipStartDate: '',
    membershipEndDate: '',
    isActive: true
  };

  constructor(
    private memberService: MemberService,
    private router: Router
  ) {}

  saveMember(form: NgForm): void {

  if (form.invalid) {

    form.control.markAllAsTouched();

    return;
  }

  console.log('Member Data:', this.member);

  this.memberService.createMember(this.member)
    .subscribe({

      next: () => {

        alert('Member Added Successfully');

        this.router.navigate(['/members']);

      },

      error: (err) => {

        console.error(err);

        alert('Failed to Add Member');

      }

    });

}
}