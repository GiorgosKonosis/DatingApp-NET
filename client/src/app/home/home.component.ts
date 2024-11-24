import { CommonModule } from '@angular/common';
import { Component, inject, input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  registered = false;
  users: any;
  http = inject(HttpClient);

  ngOnInit(): void {
    this.getUsers();
  }

  isRegistered() {
    this.registered = !this.registered;
  }

  isCancelled(event: boolean) {
    this.registered = event;
  }
  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: (next) => (this.users = next),
      error: (error) => console.log(error),
      complete: () => console.log('OK'),
    });
  }
}
