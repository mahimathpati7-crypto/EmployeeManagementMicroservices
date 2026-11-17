import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { MemberService } from '../../../core/services/member.service';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.scss'
})
export class MemberEditComponent implements OnInit {

  member: any = {};

  constructor(
    private memberService: MemberService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {

    const id = Number(
      this.route.snapshot.paramMap.get('id')
    );

    this.memberService.getMemberById(id)
      .subscribe(data => {
        this.member = data;
        this.member.membershipStartDate =
      this.member.membershipStartDate?.split('T')[0];

    this.member.membershipEndDate =
      this.member.membershipEndDate?.split('T')[0];
      });
  }

  updateMember() {

    this.memberService.updateMember(
      this.member.memberId,
      this.member
    ).subscribe(() => {

      alert('Member Updated Successfully');

      this.router.navigate(['/members']);
    });
  }
}