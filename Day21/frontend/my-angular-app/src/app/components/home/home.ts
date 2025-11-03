import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth';
import { Sidebar } from '../sidebar/sidebar';   

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule, Sidebar],   
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class Home {

  userId: number = 0;
  userName: string = '';

  constructor(private route: ActivatedRoute, private auth: AuthService) {}

  ngOnInit() {
    this.userId = Number(this.route.snapshot.paramMap.get('id'));

    if (this.userId) {
      this.auth.getUserById(this.userId).subscribe((res: any) => {
        this.userName = res?.name ?? '';
      });
    }
  }
}
