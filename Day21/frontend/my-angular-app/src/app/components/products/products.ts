import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { OrderService } from '../../services/order.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './products.html',
  styleUrls: ['./products.css']
})
export class Products {

  products: any[] = [];
  userId: number = 0;

  showForm: boolean = false;
  selectedProduct: any = null;

  qty: number = 1;
  note: string = '';
  bagOption: string = '';
  type: string = '';
  subtotal: number = 0;
  deliveryFee: number = 0;
  totalAmount: number = 0;

  constructor(
    private productService: ProductService,
    private orderService: OrderService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.route.parent?.paramMap.subscribe(params => {
      this.userId = Number(params.get("id"));
      console.log("UserId =", this.userId);
    });

    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts().subscribe({
      next: (res: any) => {
        this.products = res;
      },
      error: () => alert("Failed to load products")
    });
  }

  buyProduct(product: any) {
    this.selectedProduct = product;
    this.showForm = true;
    this.qty = 1;
    this.note = '';
    this.bagOption = '';
    this.type = '';
    this.subtotal = product.price;
    this.deliveryFee = 0;
    this.totalAmount = product.price;
  }

  calculateTotal() {
    this.subtotal = this.selectedProduct.price * this.qty;
    this.totalAmount = this.subtotal + this.deliveryFee;
  }

  submitOrder() {
    if (!this.userId || this.userId === 0) {
      alert("User not found. Please login again.");
      return;
    }

    const orderPayload = {
      id: this.userId,
      subtotal: this.subtotal,
      deliveryFee: this.deliveryFee,
      totalAmount: this.totalAmount,
      note: this.note,
      bagOption: this.bagOption,
      type: this.type
    };

    console.log("Order payload =", orderPayload);

    this.orderService.addOrder(orderPayload).subscribe({
      next: (orderId: number) => {

        if (!orderId || orderId === 0) {
          alert("Order creation failed!");
          return;
        }

        const itemPayload = {
          orderId: orderId,
          productId: this.selectedProduct.productId,
          qty: this.qty,
          price: this.selectedProduct.price
        };

        this.orderService.addOrderItem(itemPayload).subscribe({
          next: () => {
            alert("Order placed successfully!");
            this.showForm = false;
            this.router.navigate(['/home', this.userId, 'orderhistory']);
          },
          error: () => alert("Failed to add product item")
        });
      },
      error: () => {
        alert("Failed to create order");
      }
    });
  }

  cancelForm() {
    this.showForm = false;
  }
}
