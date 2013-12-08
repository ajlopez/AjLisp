
(define mapfirst (fn lst)
	(if (nil? lst)
		nil
		(cons 
			(fn (first lst)) 
			(mapfirst fn (rest lst))
		)
	)
)

(define reverse2 (lst result)
	(if (nil? lst)
		result
		(reverse2 (rest lst) (cons (first lst) result))
	)
)

(define reverse (lst)
	(reverse2 lst nil)
)
