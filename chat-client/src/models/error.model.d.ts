export type FormError = 
| {
    [key in keyof typeof initialValues]: string
  }
| null