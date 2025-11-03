import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-orderhistory',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './orderhistory.html',
  styleUrls: ['./orderhistory.css'],
})
export class Orderhistory {

  userId: number = 0;
  items: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService
  ) {}

  ngOnInit() {

    this.userId = Number(this.route.snapshot.paramMap.get("id"));

    if (!this.userId || this.userId === 0) {
      this.route.parent?.paramMap.subscribe(params => {
        this.userId = Number(params.get("id"));
      });
    }

    if (!this.userId || this.userId === 0) {
      this.userId = Number(localStorage.getItem("userId"));
    }

    console.log("OrderHistory UserId =", this.userId);

    if (!this.userId || this.userId === 0) {
      alert("User ID missing.");
      return;
    }

    this.loadItems();
  }

  loadItems() {
    this.orderService.getAllItems(this.userId).subscribe({
      next: (res) => {
        this.items = res;
      },
      error: (err) => {
        console.error("Error fetching items:", err);
      }
    });
  }
  cancelOrder(orderId: number) {
  if (!confirm("Are you sure you want to cancel this order?")) return;

  this.orderService.deleteOrder(orderId).subscribe({
    next: () => {
      alert("Order cancelled!");
      this.items = this.items.filter(o => o.orderId !== orderId);
    },
    error: (err) => {
  console.log("Delete error:", err);
  alert("Something went wrong");
  }
  });
}
}
