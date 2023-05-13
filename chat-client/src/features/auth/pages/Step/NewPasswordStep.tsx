import React, { useEffect, useState } from 'react';
import ButtonStep from './ButtonStep';
import { Box } from '@mui/material';
import { FormError } from 'models/error.model';
import * as Yup from 'yup';
import { useCreateNewPwdMutation } from 'features/auth/auth.service';
import { NewPassword } from 'models/useridentity.model';
import { Formik, Form } from 'formik';
import { useNavigate } from 'react-router-dom';
import { CssTextField } from 'utils/CssTextField';
import { isEntityError } from 'helpers';
import { minLengthPassword, routeLogin } from '../../../../constants/index';
import { toast } from 'react-toastify';

interface NewPwdStepProps {
  stepLength: number;
  activeStep: number;
  handleBack: () => void;
  email: string;
  code: string;
}

interface PwdModel {
  password: string;
  confirmPwd: string;
}

const initialValue: PwdModel = {
  password: '',
  confirmPwd: '',
};

function NewPasswordStep(props: NewPwdStepProps) {
  const navigate = useNavigate();
  const { activeStep, handleBack, stepLength, email, code } = props;
  const [createNewPwd, { data, isLoading, isSuccess }] = useCreateNewPwdMutation();
  const [formError, setFormError] = useState<FormError>(null);

  useEffect(() => {
    if (isSuccess && data) {
      toast.success('Bạn đã đổi mật khẩu thành công!');
      navigate(`/${routeLogin}`);
    }
  }, [data, isSuccess, navigate]);

  const validation = Yup.object({
    password: Yup.string()
      .required('Mật khẩu là bắt buộc.')
      .min(minLengthPassword, `Mật khẩu không được ít hơn ${minLengthPassword} ký tự`),
    confirmPwd: Yup.string()
      .oneOf([Yup.ref('password'), undefined], 'Xác nhận mật khẩu không đúng')
      .required('Xác nhận mật khẩu là bắt buộc.'),
  });

  const handleSubmit = async (values: PwdModel) => {
    try {
      setFormError(null);
      const data: NewPassword = {
        email,
        code,
        password: values.password,
        confirmPwd: values.confirmPwd
      };
      await createNewPwd(data).unwrap();
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
              fullWidth
              id="password"
              type="password"
              name="password"
              label="Mật khẩu"
              placeholder="Nhập mật khẩu"
              value={formikProps.values.password}
              onChange={formikProps.handleChange}
              error={
                (formikProps.touched.password && Boolean(formikProps.errors.password)) ||
                Boolean(formError?.password)
              }
              helperText={
                (formikProps.touched.password && formikProps.errors.password) ||
                (Boolean(formError?.password) && formError?.password)
              }
            />
            <CssTextField
              margin="normal"
              fullWidth
              id="confirmPwd"
              type="password"
              name="confirmPwd"
              label="Nhập lại mật khẩu"
              placeholder="Nhập lại mật khẩu"
              value={formikProps.values.confirmPwd}
              onChange={formikProps.handleChange}
              error={
                (formikProps.touched.confirmPwd && Boolean(formikProps.errors.confirmPwd)) ||
                Boolean(formError?.confirmPwd)
              }
              helperText={
                (formikProps.touched.confirmPwd && formikProps.errors.confirmPwd) ||
                (Boolean(formError?.confirmPwd) && formError?.confirmPwd)
              }
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
export default React.memo(NewPasswordStep);
