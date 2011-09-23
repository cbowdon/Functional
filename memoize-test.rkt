#lang racket

(require rackunit
         "memoize.rkt")

(define called-ntimes 0)

(define (sq x) 
  (begin
    (set! called-ntimes (+ called-ntimes 1))
    (* x x)))

(define x (list 1 2 3 4 5 6 7 8 9 10))

(define g (memoize sq))

(test-case
 "Called only 10 times?"
 (check-equal? called-ntimes 0)
 (map g x) 
 (check-equal? called-ntimes 10)
 (map g x)
 (check-equal? called-ntimes 10)
 (map g x)
 (check-equal? called-ntimes 10))
 

(test-case
 "Original function unchanged?"
 (check-equal? (map g x) (map sq x))
 (check-equal? (map g x) (map sq x)))




