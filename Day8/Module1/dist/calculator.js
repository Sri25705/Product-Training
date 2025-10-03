"use strict";
class Calculator {
    constructor(displayEl) {
        this.current = '';
        this.previous = '';
        this.operator = null;
        this.displayEl = displayEl;
    }
    inputDigit(d) {
        if (d === '.' && this.current.includes('.'))
            return;
        this.current += d;
        this.updateDisplay();
    }
    setOperator(op) {
        if (this.current === '' && this.previous === '')
            return;
        if (this.previous !== '') {
            this.compute();
        }
        else {
            this.previous = this.current;
        }
        this.current = '';
        this.operator = op;
    }
    compute() {
        const a = parseFloat(this.previous);
        const b = parseFloat(this.current);
        if (isNaN(a) || isNaN(b))
            return;
        let res;
        switch (this.operator) {
            case '+':
                res = a + b;
                break;
            case '-':
                res = a - b;
                break;
            case '*':
                res = a * b;
                break;
            case '/':
                if (b === 0) {
                    this.displayEl.value = "Error (÷0)";
                    this.clear();
                    return;
                }
                res = a / b;
                break;
            default: return;
        }
        this.previous = String(res);
        this.current = '';
        this.operator = null;
        this.updateDisplay();
    }
    clear() {
        this.current = '';
        this.previous = '';
        this.operator = null;
        this.updateDisplay();
    }
    updateDisplay() {
        this.displayEl.value = this.current || this.previous || '0';
    }
}
const display = document.getElementById('display');
const calc = new Calculator(display);
const buttons = [
    '7', '8', '9', '/',
    '4', '5', '6', '*',
    '1', '2', '3', '-',
    '0', '.', '=', '+',
    'C'
];
const container = document.getElementById('buttons');
buttons.forEach(label => {
    const btn = document.createElement('button');
    btn.textContent = label;
    btn.addEventListener('click', () => {
        if (!isNaN(Number(label)) || label === '.') {
            calc.inputDigit(label);
        }
        else if (label === 'C') {
            calc.clear();
        }
        else if (label === '=') {
            calc.compute();
        }
        else {
            calc.setOperator(label);
        }
    });
    container.appendChild(btn);
});
