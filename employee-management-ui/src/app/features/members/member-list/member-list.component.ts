import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { MemberService } from '../../../core/services/member.service';
import { Member } from '../../../core/models/member.model';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.scss'
})
export class MemberListComponent implements OnInit {

  members: Member[] = [];

  constructor(
    private memberService: MemberService
  ) {}

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.memberService.getMembers()
      .subscribe(data => {
        this.members = data;
      });
  }

  deleteMember(id: number) {

  if (confirm('Are you sure you want to delete this member?')) {

    this.memberService.deleteMember(id)
      .subscribe(() => {

        alert('Member Deleted Successfully');

        this.loadMembers();
      });
  }
}
}