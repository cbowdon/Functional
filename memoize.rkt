#lang racket

(provide memoize)

(define (memoize proc)
  (let ([table (make-hash)])
    (lambda (x)
      (let ([result (hash-ref table x #f)])
        (if result
            result
            (let ([ans (proc x)])
              (hash-set! table x ans)
              ans))))))


