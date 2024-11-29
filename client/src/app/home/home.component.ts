import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
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
  isRegistered() {
    this.registered = !this.registered;
  }

  isCancelled(event: boolean) {
    this.registered = event;
  }
}
