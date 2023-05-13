import React, { useEffect, useState } from 'react';
import ButtonStep from './ButtonStep';
import { Box } from '@mui/material';
import { FormError } from 'models/error.model';
import { useCheckCodeMutation } from 'features/auth/auth.service';
import * as Yup from 'yup';
import { Form, Formik } from 'formik';
import { CssLoadingButton, CssTextField } from 'utils/CssTextField';
import { VerifyCodeModel } from 'models/useridentity.model';
import { isEntityError } from 'helpers';
import EmailIcon from '@mui/icons-material/Email';

interface VerifyCodeStepProps {
  stepLength: number;
  activeStep: number;
  handleBack: () => void;
  handleNext: () => void;
  setVerifyCode: (code: string) => void;
  reSendCode: () => void;
  isLoadingResendCode: boolean;
}

const initialValue: VerifyCodeModel = {
  code: '',
};

function VerifyCodeStep(props: VerifyCodeStepProps) {
  const {
    activeStep,
    handleBack,
    handleNext,
    stepLength,
    setVerifyCode,
    reSendCode,
    isLoadingResendCode,
  } = props;
  const [checkCode, { isSuccess, data, isLoading }] = useCheckCodeMutation();
  const [formError, setFormError] = useState<FormError>(null);

  const validation = Yup.object({
    code: Yup.string().required('Vui lòng nhập mã code.'),
  });

  const handleSubmit = async (values: VerifyCodeModel) => {
    try {
      setFormError(null);
      await checkCode(values).unwrap();
    } catch (error) {
      if (isEntityError(error)) {
        const errorResult = error.data.error as FormError;
        setFormError(errorResult);
      }
    }
  };

  useEffect(() => {
    if (isSuccess && data) {
      setVerifyCode(data.code);
      handleNext();
    }
  }, [data, handleNext, setVerifyCode, isSuccess]);

  return (
    <Box sx={{ p: 2 }}>
      <Formik initialValues={initialValue} onSubmit={handleSubmit} validationSchema={validation}>
        {(formikProps) => (
          <Form autoComplete="off" style={{ marginTop: 2 }}>
            <CssTextField
              margin="normal"
              id="code"
              type="text"
              name="code"
              label="Mã xác nhận"
              placeholder="Nhập mã xác nhận"
              value={formikProps.values.code}
              onChange={formikProps.handleChange}
              error={
                (formikProps.touched.code && Boolean(formikProps.errors.code)) ||
                Boolean(formError?.code)
              }
              helperText={
                (formikProps.touched.code && formikProps.errors.code) ||
                (Boolean(formError?.code) && formError?.code)
              }
              fullWidth
            />
            <Box sx={{ textAlign: 'end' }}>
              <CssLoadingButton
                startIcon={<EmailIcon />}
                loadingPosition="start"
                loading={isLoadingResendCode}
                color="success"
                type="button"
                variant="outlined"
                onClick={reSendCode}
              >
                Gửi lại mã
              </CssLoadingButton>
            </Box>
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
export default React.memo(VerifyCodeStep);
