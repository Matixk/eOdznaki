import {Component, EventEmitter} from '@angular/core';
import {Input} from '@angular/core';
import {Output} from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent {
  @Input() currentPage: number;
  @Input() totalItems: number;
  @Input() itemsPerPage: number;
  @Input() loading: boolean;
  @Input() totalPages: number;

  @Output() goPrev = new EventEmitter<boolean>();
  @Output() goNext = new EventEmitter<boolean>();
  @Output() goPage = new EventEmitter<number>();

  constructor() { }

  getMin(): number {
    return ((this.itemsPerPage * this.currentPage) - this.itemsPerPage) + 1;
  }

  getMax(): number {
    let max = this.itemsPerPage * this.currentPage;
    if (max > this.totalItems) {
      max = this.totalItems;
    }
    return max;
  }

  getTotalPages(): number[] {
    return Array(this.totalPages).fill(0, 0, this.totalPages).map((x, i) => i + 1);
  }

  onPage(n: number): void {
    this.goPage.emit(n);
  }

  onPrev(): void {
    this.goPrev.emit(true);
  }

  onNext(next: boolean): void {
    this.goNext.emit(next);
  }

  firstPage(): boolean {
    return this.currentPage === 1;
  }

  lastPage(): boolean {
    return this.currentPage === this.totalPages;
  }

  getLinkClass(condition: boolean) {
    return condition ? 'page-link disabled' : 'page-link';
  }

}
