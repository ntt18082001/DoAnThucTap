import React, { useEffect } from 'react';
import { darkmodeColor, routeLogin } from '../../../constants';
import FormLayout from '../../../components/Layout/Client/FormLayout';
import { useAppSelector } from 'app/hooks';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';

import { styled } from '@mui/material/styles';
import Stepper from '@mui/material/Stepper';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';
import Check from '@mui/icons-material/Check';
import StepConnector, { stepConnectorClasses } from '@mui/material/StepConnector';
import { StepIconProps } from '@mui/material/StepIcon';
import { Box } from '@mui/material';
import EmailStep from './Step/EmailStep';
import VerifyCodeStep from './Step/VerifyCodeStep';
import NewPasswordStep from './Step/NewPasswordStep';
import { useForgotPasswordMutation } from '../auth.service';
import { ForgotPasswordModel } from 'models/useridentity.model';

const QontoConnector = styled(StepConnector)(({ theme }) => ({
  [`&.${stepConnectorClasses.alternativeLabel}`]: {
    top: 10,
    left: 'calc(-50% + 16px)',
    right: 'calc(50% + 16px)',
  },
  [`&.${stepConnectorClasses.active}`]: {
    [`& .${stepConnectorClasses.line}`]: {
      borderColor: '#784af4',
    },
  },
  [`&.${stepConnectorClasses.completed}`]: {
    [`& .${stepConnectorClasses.line}`]: {
      borderColor: '#784af4',
    },
  },
  [`& .${stepConnectorClasses.line}`]: {
    borderColor: theme.palette.mode === 'dark' ? theme.palette.grey[800] : '#eaeaf0',
    borderTopWidth: 3,
    borderRadius: 1,
  },
}));

const QontoStepIconRoot = styled('div')<{ ownerState: { active?: boolean } }>(
  ({ theme, ownerState }) => ({
    color: theme.palette.mode === 'dark' ? theme.palette.grey[700] : '#eaeaf0',
    display: 'flex',
    height: 22,
    alignItems: 'center',
    ...(ownerState.active && {
      color: '#784af4',
    }),
    '& .QontoStepIcon-completedIcon': {
      color: '#784af4',
      zIndex: 1,
      fontSize: 18,
    },
    '& .QontoStepIcon-circle': {
      width: 8,
      height: 8,
      borderRadius: '50%',
      backgroundColor: 'currentColor',
    },
  })
);

function QontoStepIcon(props: StepIconProps) {
  const { active, completed, className } = props;

  return (
    <QontoStepIconRoot ownerState={{ active }} className={className}>
      {completed ? (
        <Check className="QontoStepIcon-completedIcon" />
      ) : (
        <div className="QontoStepIcon-circle" />
      )}
    </QontoStepIconRoot>
  );
}

const steps = ['Xác thực email', 'Mã xác thực', 'Đổi mật khẩu mới'];

function ForgotPassword() {
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const [activeStep, setActiveStep] = React.useState(0);
  const [skipped, setSkipped] = React.useState(new Set<number>());
  const [email, setEmail] = React.useState('');
  const [code, setCode] = React.useState('');
  const [forgotPassword, { isLoading }] = useForgotPasswordMutation();

  useEffect(() => {
    document.title = 'Quên mật khẩu';
  }, []);

  const isStepSkipped = (step: number) => {
    return skipped.has(step);
  };

  const handleNext = () => {
    if (activeStep === steps.length - 1) return;
    let newSkipped = skipped;
    if (isStepSkipped(activeStep)) {
      newSkipped = new Set(newSkipped.values());
      newSkipped.delete(activeStep);
    }

    setActiveStep((prevActiveStep) => prevActiveStep + 1);
    setSkipped(newSkipped);
  };

  const handleBack = () => {
    setActiveStep((prevActiveStep) => prevActiveStep - 1);
  };

  const handleResendCode = async () => {
    try {
      const values: ForgotPasswordModel = {
        email
      };
      await forgotPassword(values).unwrap();
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <FormLayout title="Quên mật khẩu" titleUrl="Đăng nhập" urlLink={routeLogin}>
      <Box sx={{ width: '100%', mb: 2, mt: 2 }}>
        <Stepper activeStep={activeStep} alternativeLabel connector={<QontoConnector />}>
          {steps.map((label, index) => {
            const stepProps: { completed?: boolean } = {};
            if (isStepSkipped(index)) {
              stepProps.completed = false;
            }
            return (
              <Step key={label} {...stepProps}>
                <StepLabel
                  StepIconComponent={QontoStepIcon}
                  sx={{
                    '.css-1hv8oq8-MuiStepLabel-label.MuiStepLabel-alternativeLabel': {
                      color: isDarkmode ? darkmodeColor : '',
                    },
                    '.css-1hv8oq8-MuiStepLabel-label.Mui-active': {
                      color: '#784af4',
                    },
                    '.css-1hv8oq8-MuiStepLabel-label.Mui-completed': {
                      color: '#784af4 !important',
                    },
                  }}
                >
                  {label}
                </StepLabel>
              </Step>
            );
          })}
        </Stepper>
        <React.Fragment>
          {activeStep === 0 && (
            <EmailStep
              activeStep={activeStep}
              handleBack={handleBack}
              handleNext={handleNext}
              stepLength={steps.length}
              handleSetEmail={setEmail}
            />
          )}
          {activeStep === 1 && (
            <VerifyCodeStep
              activeStep={activeStep}
              handleBack={handleBack}
              handleNext={handleNext}
              stepLength={steps.length}
              setVerifyCode={setCode}
              reSendCode={handleResendCode}
              isLoadingResendCode={isLoading}
            />
          )}
          {activeStep === steps.length - 1 && (
            <NewPasswordStep
              activeStep={activeStep}
              handleBack={handleBack}
              stepLength={steps.length}
              email={email}
              code={code}
            />
          )}
        </React.Fragment>
      </Box>
    </FormLayout>
  );
}
export default React.memo(ForgotPassword);
