import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin.html',
  styleUrls: ['./admin.css'],
})
export class Admin {

  products: any[] = [];

  newProduct: any = {
    productName: '',
    price: ''
  };

  constructor(
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadProducts();
  }

  goBack() {
    this.router.navigate(['/login']);
  }

  loadProducts() {
    this.productService.getProducts().subscribe({
      next: (res) => this.products = res,
      error: () => alert("Failed to load products")
    });
  }

  addProduct() {
    if (!this.newProduct.productName || !this.newProduct.price) {
      alert("All fields required!");
      return;
    }

    this.productService.addProduct(this.newProduct).subscribe({
      next: () => {
        alert("Product added!");
        this.newProduct = { productName: '', price: '' };
        this.loadProducts();
      },
      error: () => alert("Failed!")
    });
  }

}
