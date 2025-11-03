import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { OrderService } from '../../services/order.service';
import { AuthService } from '../../services/auth';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-liveorders',
  standalone: true,
  imports: [CommonModule, DatePipe,FormsModule],
  templateUrl: './liveorders.html',
  styleUrls: ['./liveorders.css'],
})
export class Liveorders {

  userId: number = 0;
  order: any = null;
  items: any[] = [];
  customer: any = null;
  showModal = false;
  length="";

  editAddress = {
    
    addressLine: '',
    buildingName: '',
    street: '',
    postalCode: ''
  };

  constructor(private route: ActivatedRoute, private orderService: OrderService,private auth:AuthService ) {}

  ngOnInit() {
    this.userId = Number(this.route.snapshot.paramMap.get("id"));

    if (!this.userId) {
      this.route.parent?.paramMap.subscribe(params => {
        this.userId = Number(params.get("id"));
        if (!this.userId) {
          this.userId = Number(localStorage.getItem("userId"));
        }
        this.loadOrder();
      });
    } else {
      this.loadOrder();
    }
  }

  loadOrder() {
    if (!this.userId || this.userId === 0) {
      console.error("User ID missing");
      return;
    }
    
    this.orderService.getOrderDetails(this.userId).subscribe({
      next: (res) => {
        this.order = res.order;
        this.items = res.items;
        this.customer = res.customer;
      },
      error: (err) => {
        console.error("Error loading order:", err);
      }
    });
  }
  
  openEditModal() {
    this.editAddress = {
      addressLine: this.customer?.addressLine,
      buildingName: this.customer?.buildingName,
      street: this.customer?.street,
      postalCode: this.customer?.postalCode
    };
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
  }

  saveAddress() {
    const payload = {
       ...this.editAddress,
      id: this.customer.id,
      name: this.customer.name,
      phoneno: this.customer.phoneno,
      password: this.customer.password,
     
    };

    this.auth.updateUser(payload).subscribe({
      next: () => {
        alert("Address updated successfully!");
        this.showModal = false;
        this.loadOrder();
      },
      error: () => alert("Failed to update address")
    });
  }
}
