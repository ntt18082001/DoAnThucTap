import React, { useEffect, useState } from 'react';
import ButtonStep from './ButtonStep';
import { Box } from '@mui/material';
import { Form, Formik } from 'formik';
import * as Yup from 'yup';
import { CssTextField } from 'utils/CssTextField';
import { FormError } from 'models/error.model';
import { ForgotPasswordModel } from 'models/useridentity.model';
import { useForgotPasswordMutation } from 'features/auth/auth.service';
import { isEntityError } from 'helpers';

interface EmailStepProps {
  stepLength: number;
  activeStep: number;
  handleBack: () => void;
  handleNext: () => void;
  handleSetEmail: (email: string) => void;
}

const initialValue: ForgotPasswordModel = {
  email: '',
};

function EmailStep(props: EmailStepProps) {
  const { activeStep, handleBack, handleNext, stepLength, handleSetEmail } = props;
  const [forgotPassword, { data, isLoading, isSuccess }] = useForgotPasswordMutation();
  const [formError, setFormError] = useState<FormError>(null);

  const validation = Yup.object({
    email: Yup.string().required('Vui lòng nhập email.').email('Vui lòng nhập một email hợp lệ.'),
  });

  useEffect(() => {
    if (isSuccess && data) {
      handleSetEmail(data.email);
      handleNext();
    }
  }, [data, handleNext, handleSetEmail, isSuccess]);

  const handleSubmit = async (values: ForgotPasswordModel) => {
    try {
      setFormError(null);
      await forgotPassword(values).unwrap();
    } catch (error) {
      if (isEntityError(error)) {
        const errorResult = error.data.error as FormError;
        setFormError(errorResult);
      }
    }
  };

  return (
    <Box sx={{ p: 2 }}>
      <Formik initialValues={initialValue} onSubmit={handleSubmit} validationSchema={validation}>
        {(formikProps) => (
          <Form autoComplete="off" style={{ marginTop: 2 }}>
            <CssTextField
              margin="normal"
              id="email"
              type="email"
              name="email"
              label="Email"
              placeholder="Nhập email"
              value={formikProps.values.email}
              onChange={formikProps.handleChange}
              error={
                (formikProps.touched.email && Boolean(formikProps.errors.email)) ||
                Boolean(formError?.email)
              }
              helperText={
                (formikProps.touched.email && formikProps.errors.email) ||
                (Boolean(formError?.email) && formError?.email)
              }
              fullWidth
            />
            <ButtonStep
              activeStep={activeStep}
              handleBack={handleBack}
              stepLength={stepLength}
              isLoading={isLoading}
              type="submit"
            />
          </Form>
        )}
      </Formik>
    </Box>
  );
}
export default React.memo(EmailStep);
