import React, { useState } from 'react';
import { InputAdornment, Typography } from '@mui/material';
import { CssTextField } from 'utils/CssTextField';
import SearchIcon from '@mui/icons-material/Search';
import { useDebounce } from 'hooks/useDebounce';
import UserList from './UserList';

function User() {
  const [searchTerm, setSearchTerm] = useState('');
  const debounceSearchTerm = useDebounce(searchTerm, 1000);

  return (
    <>
      <Typography variant="h4" gutterBottom sx={{ textAlign: 'center' }}>
        Danh sách user
      </Typography>
      <CssTextField
        id="search"
        type="search"
        label="Tìm kiếm"
        placeholder="Nhập tên..."
        value={searchTerm}
        onChange={(ev) => setSearchTerm(ev.target.value)}
        sx={{ width: 500, mt: 2, mb: 2 }}
        InputProps={{
          endAdornment: (
            <InputAdornment position="end">
              <SearchIcon color="secondary" />
            </InputAdornment>
          ),
        }}
      />
      <UserList searchTerm={debounceSearchTerm} />
    </>
  )
}
export default React.memo(User);